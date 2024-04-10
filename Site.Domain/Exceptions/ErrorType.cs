using System.ComponentModel;

namespace Site.Domain.Exceptions
{
    public enum ErrorType
    {

        [Description("خطا در برقراری ارتباط")]
        ConnectionIsFailed = 1001,


    }
}