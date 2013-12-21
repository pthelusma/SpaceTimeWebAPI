using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class TaskController : ApiController
    {

        public Task GetTask(int id)
        {

            SpaceTimeEntities context = null;

            context = new SpaceTimeEntities();

            return context.Tasks.Where(t => t.task_id == id).Single();

        }

        public Task SetTask([FromBody] Task value)
        {
           
            SpaceTimeEntities context = null;
            context = new SpaceTimeEntities();

            Guid taskGuid;

            if (value.task_id == 0)
            {
                value.alternate_id = Guid.NewGuid();
                value.active = true;

                foreach (TaskLocation taskLocation in value.TaskLocations)
                {
                    taskLocation.alternate_id = Guid.NewGuid();
                }

                context.Tasks.Add(value);

                taskGuid = value.alternate_id;
            }
            else 
            {
                
                Task task = context.Tasks.Where(t => t.task_id == value.task_id).Single();

                task.title = value.title;
                task.due_date = value.due_date;
                task.update_date = value.update_date;
                task.active = value.active;

                if (task.TaskLocations.Count == 1 && value.TaskLocations.Count == 1)
                {
                    task.TaskLocations.Where(tl => tl.task_id == task.task_id).Single().radius_id = value.TaskLocations.Where(tl => tl.task_id == task.task_id).Single().radius_id;
                    task.TaskLocations.Where(tl => tl.task_id == task.task_id).Single().location_id = value.TaskLocations.Where(tl => tl.task_id == task.task_id).Single().location_id;
                }

                if (task.TaskLocations.Count == 0 && value.TaskLocations.Count == 1)
                {
                    task.TaskLocations.Add(
                        new TaskLocation
                        {
                            radius_id = value.TaskLocations.Single().radius_id,
                            location_id = value.TaskLocations.Single().location_id,
                            alternate_id = Guid.NewGuid()
                        });
                }

                taskGuid = task.alternate_id;
            }

            context.SaveChanges();

            return context.Tasks.Where(t => t.alternate_id == taskGuid).Single();
        }

        public IEnumerable<Location> GetLocations()
        {
            SpaceTimeEntities context = null;

            context = new SpaceTimeEntities();

            return context.Locations.ToList();
        }

        public IEnumerable<Task> GetTasks()
        {
            SpaceTimeEntities context = null;

            context = new SpaceTimeEntities();

            return context.Tasks.ToList();
        }

        public IEnumerable<Radius> GetRadii()
        {
            SpaceTimeEntities context = null;

            context = new SpaceTimeEntities();

            return context.Radii.ToList();
        }


    }
}