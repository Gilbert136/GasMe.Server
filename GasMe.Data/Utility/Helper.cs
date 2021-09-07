using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GasMe.Data.Enums;
using GasMe.Data.Models.EntityBase;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;



namespace GasMe.Data.Models
{
    public class EnumCollectionJsonValueConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
    {
        public EnumCollectionJsonValueConverter() : base(
            v => JsonConvert.SerializeObject(v.Select(e => e.ToString()).ToList()),
            v => JsonConvert.DeserializeObject<ICollection<string>>(v).Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
        {
        }
    }
    public class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
    {
        public CollectionValueComparer() : base(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => (ICollection<T>)c.ToHashSet())
        {
        }
    }
}
