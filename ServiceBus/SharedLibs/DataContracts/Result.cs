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

        public static Result Success(string message = null)
        {
            return new Result { ResultType = ResultType.Success, Message = message };
        }

        public static Result Error(string message)
        {
            return new Result { ResultType = ResultType.Error, Message = message };
        }

        public static Result Warning(string message)
        {
            return new Result { ResultType = ResultType.Warning, Message = message };
        }

        public static Result Fatal(string message)
        {
            return new Result { ResultType = ResultType.Fatal, Message = message };
        }

        #endregion
    }
}