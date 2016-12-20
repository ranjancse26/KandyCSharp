using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class GroupEntity
    {
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("group_image")]
        public string GroupImage { get; set; }
    }

    public class GroupImage
    {
    }

    public class Owner
    {
        [JsonProperty("full_user_id")]
        public string FullUserId { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }
    }

    public class Member
    {
        [JsonProperty("full_user_id")]
        public string FullUserId { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }
    }

    public class GroupResultEntity
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("group_image")]
        public GroupImage GroupImage { get; set; }

        [JsonProperty("max_members")]
        public string MaxMembers { get; set; }

        [JsonProperty("owners")]
        public List<Owner> Owners { get; set; }

        [JsonProperty("creation_time")]
        public long CreationTime { get; set; }

        [JsonProperty("members")]
        public List<Member> Members { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }
    }

    public class GroupResultRootEntity : BaseEntity
    {
        [JsonProperty("result")]
        public GroupResultEntity GroupResultEntity { get; set; }
    }

    public class AllGroupResultEntity : BaseEntity
    {
        [JsonProperty("groups")]
        public List<GroupResultEntity> GroupResultEntity { get; set; }

        [JsonProperty("status_description")]
        public StatusDescription StatusDescription { get; set; }
    }

    public class AllGroupResultRootEntity : BaseEntity
    {
        [JsonProperty("result")]
        public AllGroupResultEntity AllGroupResultEntity { get; set; }

        [JsonProperty("status_description")]
        public StatusDescription StatusDescription { get; set; }
    }
}
