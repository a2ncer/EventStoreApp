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
  public class CreateSensorCommand {
    /// <summary>
    /// Gets or Sets FarmId
    /// </summary>
    [DataMember(Name="farmId", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "farmId")]
    public Guid? FarmId { get; set; }

    /// <summary>
    /// Gets or Sets State
    /// </summary>
    [DataMember(Name="state", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "state")]
    public SensorState State { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class CreateSensorCommand {\n");
      sb.Append("  FarmId: ").Append(FarmId).Append("\n");
      sb.Append("  State: ").Append(State).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
