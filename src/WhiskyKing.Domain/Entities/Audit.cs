namespace WhiskyKing.Domain.Entities;

public class Audit : BaseEntityMin
{
    private Audit()
    {
    }

    public Audit(string tableName, string keyValue, byte entitySate, Guid registerUserId, string oldValuesJson, string newValuesJson)
    {
        TableName = tableName;
        KeyValue = keyValue;
        EntityState = entitySate;
        OldValuesJson = oldValuesJson;
        NewValuesJson = newValuesJson;
        SetRegisterUser(registerUserId);
    }

    public string TableName { get; private set; } = default!;
    public string KeyValue { get; private set; } = default!;
    public byte EntityState { get; private set; }
    public string OldValuesJson { get; private set; } = default!;
    public string NewValuesJson { get; private set; } = default!;
}