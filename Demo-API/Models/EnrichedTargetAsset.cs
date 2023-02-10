namespace Demo_API.Models
{
    public class EnrichedTargetAsset
    {
        public int id { get; set; }
        public bool? isStartable { get; set; }
        public string location { get; set; }
        public string owner { get; set; }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public List<string>? tags { get; set; }
        public int cpu { get; set; }
        public long ram { get; set; }
        public DateTime? createdAt { get; set; }
        public int? parentId { get; set; }

        public int parentTargetAssetCount { get; set; }
    }

    public static class TargetAssetExtension
    {
        public static EnrichedTargetAsset? toEnnrichedTargetAsset(this TargetAsset obj)
        {
            if (obj == null) 
                return null;
            else
            return new EnrichedTargetAsset
            {
                id = obj.id,
                isStartable = obj.isStartable,
                location = obj.location,
                owner = obj.owner,
                createdBy = obj.createdBy,
                name = obj.name,
                status = obj.status,
                tags = obj.tags != null ? obj.tags.Select(x => (string)x.Clone()).ToList<string>() : null,
                cpu = obj.cpu,
                ram = obj.ram,
                createdAt = obj.createdAt,
                parentId = obj.parentId,
                parentTargetAssetCount = 0
            };
        }
    }
}
