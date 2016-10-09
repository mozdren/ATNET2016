using System.Runtime.Serialization;
using SharedLibs.Enums;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Basic datacontract representing result of any call. Should be part of each DTO
    /// </summary>
    [DataContract]
    public sealed class Result
    {
        #region Properties

        /// <summary>
        /// True if call was successful (uses ResultType, cannot be set)
        /// </summary>
        [IgnoreDataMember]
        public bool IsSuccess {
            get
            {
                return ResultType == ResultType.Success;
            }
        }

        /// <summary>
        /// true if call was not successful - uses IsSuccess (cannot be set)
        /// </summary>
        [IgnoreDataMember]
        public bool IsNotSuccess {
            get
            {
                return !IsSuccess;
            }
        }

        /// <summary>
        /// Optional message about the result
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Type of result. Success, Error, Warning etc.
        /// </summary>
        [DataMember]
        public ResultType ResultType { get; set; }

        #endregion

        #region Factories

        /// <summary>
        /// Creates a success result with info message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>success result</returns>
        public static Result Success(string message = null)
        {
            return new Result { ResultType = ResultType.Success, Message = message };
        }

        /// <summary>
        /// Creates an Error result with info message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>Error result</returns>
        public static Result Error(string message)
        {
            return new Result { ResultType = ResultType.Error, Message = message };
        }

        /// <summary>
        /// Creates an Warning result with info message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>Warning result</returns>
        public static Result Warning(string message)
        {
            return new Result { ResultType = ResultType.Warning, Message = message };
        }

        /// <summary>
        /// Creates an Fatal result with info message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>Fatal result</returns>
        public static Result Fatal(string message)
        {
            return new Result { ResultType = ResultType.Fatal, Message = message };
        }

        /// <summary>
        /// Creates an Success result with info message
        /// </summary>
        /// <param name="messageFormat">message format</param>
        /// <param name="parameters">format data</param>
        /// <returns>success result</returns>
        public static Result SuccessFormat(string messageFormat, params object[] parameters)
        {
            return new Result { ResultType = ResultType.Success, Message = string.Format(messageFormat, parameters) };
        }

        /// <summary>
        /// Creates an Error result with info message
        /// </summary>
        /// <param name="messageFormat">message format</param>
        /// <param name="parameters">format data</param>
        /// <returns>error result</returns>
        public static Result ErrorFormat(string messageFormat, params object[] parameters)
        {
            return new Result { ResultType = ResultType.Error, Message = string.Format(messageFormat, parameters) };
        }

        /// <summary>
        /// Creates an Warning result with info message
        /// </summary>
        /// <param name="messageFormat">message format</param>
        /// <param name="parameters">format data</param>
        /// <returns>warning result</returns>
        public static Result WarningFormat(string messageFormat, params object[] parameters)
        {
            return new Result { ResultType = ResultType.Warning, Message = string.Format(messageFormat, parameters) };
        }

        /// <summary>
        /// Creates an Fatal result with info message
        /// </summary>
        /// <param name="messageFormat">message format</param>
        /// <param name="parameters">format data</param>
        /// <returns>fatal result</returns>
        public static Result FatalFormat(string messageFormat, params object[] parameters)
        {
            return new Result { ResultType = ResultType.Fatal, Message = string.Format(messageFormat, parameters) };
        }

        #endregion
    }
}