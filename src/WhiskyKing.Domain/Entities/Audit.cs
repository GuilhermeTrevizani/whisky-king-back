namespace WhiskyKing.Domain.Entities;

public class Audit : BaseEntityMin
{
    public string TableName { get; private set; } = string.Empty;
    public string KeyValue { get; private set; } = string.Empty;
    public byte EntityState { get; private set; }
    public string OldValuesJson { get; private set; } = string.Empty;
    public string NewValuesJson { get; private set; } = string.Empty;

    public void Create(string tableName, string keyValue, byte entitySate, Guid registerUserId, string oldValuesJson, string newValuesJson)
    {
        TableName = tableName;
        KeyValue = keyValue;
        EntityState = entitySate;
        OldValuesJson = oldValuesJson;
        NewValuesJson = newValuesJson;
        SetRegisterUser(registerUserId);
    }
}