﻿using System.Net;

namespace HouseReservationWebAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public Object Result { get; set; }
    }
}
