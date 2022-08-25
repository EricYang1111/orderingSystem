﻿namespace OrderingSystem.Models
{
    public class OrderToClient
    {
        public int id = 0;
        public int cuid = 0;
        public double totalPrice = 0;
        public DateTime date = DateTime.MinValue;
        public int status = 0;
        public string address = "";
        public string telephone = "";
    }
}
