using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
   public class Response
  {
        public ResponseStatus Status = ResponseStatus.Success;
        public List<string> Msgs = new List<string>();
        public Response()
        {
        }
        public Response(ResponseStatus p_Status, string Msg)
        {
            Status = p_Status;
            Msgs.Add(Msg);
        }
        public Response AddMsg(string Msg)
        {
            Msgs.Add(Msg);
            return this;
        }
    }
  public enum ResponseStatus
    {
        Success =0,
        Error=1
    }
}
