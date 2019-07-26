using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.Domain.Model
{
    public class MessageSearch
    {
        public MessageType? MessageType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public virtual string ToWhereString()
        {
            StringBuilder where = new StringBuilder();
            if (MessageType.HasValue)
            {
                where.Append(" MessageType = @MessageType ");
            }

            if (StartTime.HasValue)
            {
                if (where.Length > 0)
                    where.Append(" and ");
                where.Append(" CreateTime > @StartTime ");
            }
            if (EndTime.HasValue)
            {
                if (where.Length > 0)
                    where.Append(" and ");
                where.Append(" CreateTime < @EndTime ");
            }

            if (where.Length > 0)
                where.Insert(0, " where ");

            return where.ToString();
        }

        public virtual object ToParams()
        {
            dynamic obj = new ExpandoObject();

            obj.PageIndex = PageIndex;
            obj.PageSize = PageSize;

            if (MessageType.HasValue)
                obj.MessageType = (int)MessageType.Value;

            if (StartTime.HasValue)
                obj.StartTime = StartTime.Value;

            if (EndTime.HasValue)
                obj.EndTime = EndTime.Value;

            return obj;
        }
    }
}
