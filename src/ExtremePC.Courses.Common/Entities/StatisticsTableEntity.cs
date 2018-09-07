using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.Common.Entities
{
    public class StatisticsTableEntity<T> : TableEntity
    {
        public StatisticsTableEntity()
        { }

        public StatisticsTableEntity(string partitionKey, string rowKey, T value) : base(partitionKey, rowKey)
        {
            Value = value;
        }

        public T Value { get; set; }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var entityProperties = base.WriteEntity(operationContext);

            if (!entityProperties.ContainsKey(nameof(Value)))
            {
                entityProperties[nameof(Value)] = new EntityProperty(JsonConvert.SerializeObject(Value));
            }

            return entityProperties;
        }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);

            if (!properties.ContainsKey(nameof(Value)))
                return;

            var propertyType = properties[nameof(Value)].PropertyAsObject.GetType();
            if (typeof(T) == propertyType || propertyType != typeof(string))
                return;

            try
            {
                Value = JsonConvert.DeserializeObject<T>(properties[nameof(Value)].StringValue);
            }
            catch { }
        }
    }
}
