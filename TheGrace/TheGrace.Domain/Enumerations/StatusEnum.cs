using Ardalis.SmartEnum;

namespace TheGrace.Domain.Enumerations;

public class StatusEnum : SmartEnum<StatusEnum>
{
    public StatusEnum(string name, int value) : base(name, value) { }

    #region ==========================  StatusEnum ==========================
    public static readonly StatusEnum InActive = new(nameof(InActive), 0);
    public static readonly StatusEnum Active = new(nameof(Active), 1);
    #endregion ==========================  StatusEnum ==========================

    public static implicit operator StatusEnum(string name) => FromName(name);

    public static implicit operator StatusEnum(int value) => FromValue(value);

    public static implicit operator string(StatusEnum type) => type.Name;

    public static implicit operator int(StatusEnum type) => type.Value;
}
