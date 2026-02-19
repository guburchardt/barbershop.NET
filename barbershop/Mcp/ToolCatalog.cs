namespace barbershop.Mcp;
public sealed record ToolDefinition(string Name, string Description, object InputSchema);

public static class ToolCatalog
{
    public static IReadOnlyList<ToolDefinition> All => new List<ToolDefinition>
    {
        new(
            Name: "clients.list",
            Description: "List all clients.",
            InputSchema: new { type = "object", properties = new { } }
        ),
        new(
            Name: "clients.create",
            Description: "Create a new client.",
            InputSchema: new
            {
                type = "object",
                properties = new
                {
                    fullName = new { type = "string" },
                    phone = new { type = "string", nullable = true },
                    email = new { type = "string", nullable = true }
                },
                required = new[] { "fullName" }
            }
        )
    };
}
