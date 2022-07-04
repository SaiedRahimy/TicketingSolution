using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Core.Enums;
using Xunit.Sdk;

namespace TicketingSolution.Core
{
    public class SuccessOrFailureDataAttribute : DataAttribute
    {
        public override object TypeId => base.TypeId;

        public override string Skip { get => base.Skip; set => base.Skip = value; }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { BookingResultFlag.Failure, false };
            yield return new object[] { BookingResultFlag.Success, true };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }

        public override bool Match(object? obj)
        {
            return base.Match(obj);
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
