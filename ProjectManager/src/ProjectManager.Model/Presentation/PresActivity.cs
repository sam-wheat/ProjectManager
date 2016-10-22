using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using ProjectManager.Model.Domain;

namespace ProjectManager.Model.Presentation
{
    //[DataContract]
    public class PresActivity : BasePresentationModel
    {
        //[DataMember]
        public long ID { get; set; }
        //[DataMember]
        public int UserID { get; set; }
        //[DataMember]
        public Nullable<int> ProjectID { get; set; }
        //[DataMember]
        public string ProjectName { get; set; }
        //[DataMember]
        public Nullable<int> ContactID { get; set; }
        //[DataMember]
        public DateTime Date { get; set; }
        //[DataMember]
        public string Notes { get; set; }
        //[DataMember]
        public string Media { get; set; } // i.e. Phone, text, email etc.
        //[DataMember]
        public string Tags { get; set; }

        public PresActivity()
        { 
        }

        public PresActivity(Activity activity)
        {
            ID = activity.ID;
            UserID = activity.UserID;
            ProjectID = activity.ProjectID;
            ProjectName = activity.Project == null ? "" : activity.Project.Name;
            ContactID = activity.ContactID;
            Date = activity.Date;
            Notes = activity.Notes;
            Media = activity.Media;
            Tags = activity.Tags;
        }

        public Activity ToActivity()
        {
            return new Activity
            {
                ID = ID,
                UserID = UserID,
                ProjectID = ProjectID,
                ContactID = ContactID,
                Date = Date,
                Notes = Notes,
                Media = Media,
                Tags = Tags,
            };
        }
    }
}
