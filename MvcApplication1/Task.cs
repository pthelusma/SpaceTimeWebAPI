//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public Task()
        {
            this.TaskLocations = new HashSet<TaskLocation>();
        }
    
        public int task_id { get; set; }
        public string title { get; set; }
        public System.DateTime create_date { get; set; }
        public System.Guid alternate_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Nullable<System.DateTime> due_date { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public bool active { get; set; }
    
        public virtual ICollection<TaskLocation> TaskLocations { get; set; }
    }
}
