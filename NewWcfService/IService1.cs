using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Student> GetInfo(Student student);

        [OperationContract]
        List<Student> InnerJoin(JSONMsg msg);
    }

    [DataContract]
    public class Student
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int addressId { get; set; }
    }

    [DataContract]
    public class JSONMsg
    {
        [DataMember]
        public string token { get; set; }
    }

}
