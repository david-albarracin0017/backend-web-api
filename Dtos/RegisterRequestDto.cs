﻿namespace MicroService_NaceTuIdea.Dtos
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool HasProperty { get; set; }
    }
}
