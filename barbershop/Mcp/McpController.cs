using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using barbershop.Application.UseCases.Clients.ListClients;
using barbershop.Application.UseCases.Clients.CreateClient;

namespace barbershop.Mcp
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class McpController : ControllerBase
    {
        private readonly ListClientsHandler _listClients;
        private readonly CreateClientHandler _createClients;

        public McpController(ListClientsHandler listClients, CreateClientHandler createClients)
        {
            _listClients = listClients;
            _createClients = createClients;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonRpcRequest req, CancellationToken ct)
        {
            return req.Method switch
            {
                "initialize" => Ok(JsonRpcResponse.Ok(req.Id, new
                {
                    serverInfo = new { name = "barbershop-mcp", version = "0.1.0" },
                    capabilities = new { tools = new { } }
                })),

                "tools/list" => Ok(JsonRpcResponse.Ok(req.Id, new
                {
                    tools = ToolCatalog.All
                })),

                "tools/call" => Ok(await HandleToolCallAsync(req, ct)),

                _ => Ok(JsonRpcResponse.Fail(req.Id, -32601, $"Method not found: {req.Method}"))
            };
        }

        private async Task<JsonRpcResponse> HandleToolCallAsync(JsonRpcRequest req, CancellationToken ct)
        {
            if (req.Params is null)
                return JsonRpcResponse.Fail(req.Id, -32602, "Missing params");

            var p = req.Params.Value;

            if (p.ValueKind != JsonValueKind.Object)
                return JsonRpcResponse.Fail(req.Id, -32602, "params must be an object");

            if (!p.TryGetProperty("name", out var nameEl))
                return JsonRpcResponse.Fail(req.Id, -32602, "Missing params.name");

            var toolName = nameEl.GetString();
            if (string.IsNullOrWhiteSpace(toolName))
                return JsonRpcResponse.Fail(req.Id, -32602, "Invalid params.name");

            if (toolName == "clients.list")
            {
                var result = await _listClients.Handle(new ListClientsQuery(), ct);

                var payload = result.Select(c => new
                {
                    id = c.Id,
                    fullName = c.FullName,
                    isActive = c.IsActive
                });

                return JsonRpcResponse.Ok(req.Id, new
                {
                    content = new object[]
                    {
                        new { type = "json", value = payload }
                    }
                });
            }

            return JsonRpcResponse.Ok(req.Id, new
            {
                content = new object[]
                {
                    new { type = "text", text = $"Tool '{toolName}' not implemented yet." }
                }
            });
        }
    }
}
