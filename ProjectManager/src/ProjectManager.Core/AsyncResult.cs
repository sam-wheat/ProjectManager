using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ProjectManager.Core
{
    [DataContract]
    public class AsyncResult : IAsyncServiceResult
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public int Result { get; set; }  // May be an ID or count
    }

    [DataContract]
    public class AsyncResult<T> : AsyncResult, IAsyncServiceResult<T>
    {
        [DataMember]
        public T Data { get; set; }
    }
}
