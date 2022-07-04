using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Core.Enums;

namespace TicketingSolution.Core
{
    public class TestDataShare
    {
        public static IEnumerable<object[]> SuccessOrFailureData
        {
            get
            {
                yield return new object[] { BookingResultFlag.Failure, false };
                yield return new object[] { BookingResultFlag.Success, true };

            }
        }
    }
}
