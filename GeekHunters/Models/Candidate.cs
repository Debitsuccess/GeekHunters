using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GeekHunters.Models
{
    public class Candidate
    {
        Guid _id = Guid.Empty;
        string[] _technologies = null;

        [Required]
        [JsonProperty(PropertyName = "id")]
        public Guid ID
        {
            get
            {
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [Required]
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "technologies")]
        public string[] Technologies
        {
            get
            {
                if (_technologies == null)
                {
                    _technologies = new string[] { };
                }
                return _technologies;
            }
            set
            {
                _technologies = value;
            }
        }
    }
}