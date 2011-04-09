using System;
using System.Text;

namespace FCOTravelAlerts.Service.Entity
{
    public class Item
    {
        private readonly string _title;
        private readonly DateTime _datePublished;

        public Item(string title, DateTime datePublished)
        {
            _title = title;
            _datePublished = datePublished;
        }

        public DateTime DatePublished
        {
            get { return _datePublished; }
        }

        public string Title
        {
            get { return _title; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Title: {0} DatePublished: {1}", Title, DatePublished);
            return sb.ToString();
        }
    }
}