using System;
using System.ComponentModel;
using BusinessLogic.Constants;

namespace BusinessLogic.Response
{
    public record BaseResponce
    {
        [DefaultValue("SUCCESS")]
        public string Message {get; init;}

        [DefaultValue(true)]
        public bool Success {get; init;}

        public static BaseResponce SuccessResponce => new ()
        {
            Message = ResponceMessageCodes.Success,
            Success = true,
        };
    }

    public abstract record BaseResponce<T> : BaseResponce where T : BaseResponce;
}