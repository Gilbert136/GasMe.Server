using System;

namespace GasMe.Data.Enums
{
    public enum EntityStatus : byte
    {
        New = 1,
        Active,
        Delete,
        Remove,
    }

    public enum TransactionStatus : byte
    {
        Pending = 1,
        delivered,
        cancelled,
    }

    public enum UnitClasification : byte
    {
        Currency = 1,
        Mass,
    }
}