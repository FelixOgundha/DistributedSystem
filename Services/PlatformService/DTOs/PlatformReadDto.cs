﻿using System.ComponentModel.DataAnnotations;

namespace PlatformService.DTOs
{
    public class PlatformReadDto
    {
       
        public int Id { get; set; }
       
        public string Name { get; set; } = string.Empty;
       
        public string Publisher { get; set; } = string.Empty;
       
        public string Cost { get; set; } = string.Empty;
    }
}
