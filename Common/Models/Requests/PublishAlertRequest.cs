using Common.Interfaces.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Requests
{
public class PublishAlertRequest
{

     public string AlertType { get ; set; }
    public DateTime PublishRequestTime { get; set; }

}
}

// Path: Common/Models/Requests/SubscribeRequest.cs