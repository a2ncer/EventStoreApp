using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WebApiClient.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public enum SensorState
  {
        Inventory,
        Deployed,
        FarmerTriage,
        Returned,
        Dead,
        Refurbished
  }
}
