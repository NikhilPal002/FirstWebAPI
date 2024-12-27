using System.ComponentModel.DataAnnotations;

namespace FirstWebAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "code has to be minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "code has to be minimum 3 characters")]

        public string Code 
        {
            get; set;
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Name has to be maximum 50 characters")]

        public string Name
        {
            get; set;
        }
        public string? RegionImageUrl
        {
            get; set;
        }
    }
}
