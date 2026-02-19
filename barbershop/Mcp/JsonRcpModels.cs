using System.Text.Json;

namespace barbershop.Mcp;
public sealed class JsonRpcRequest
{
    public string Jsonrpc { get; set; } = "2.0";
    public string Method { get; set; } = default!;
    public JsonElement? Params { get; set; }
    public JsonElement? Id { get; set; }
}

public sealed class JsonRpcResponse
{
    public string Jsonrpc { get; set; } = "2.0";
    public JsonElement? Id { get; set; }
    public object? Result { get; set; }
    public JsonRpcError? Error { get; set; }

    public static JsonRpcResponse Ok(JsonElement? id, object result) =>
        new() { Id = id, Result = result };

    public static JsonRpcResponse Fail(JsonElement? id, int code, string message) =>
        new() { Id = id, Error = new JsonRpcError { Code = code, Message = message } };
}

public sealed class JsonRpcError
{
    public int Code { get; set; }
    public string Message { get; set; } = default!;
}
