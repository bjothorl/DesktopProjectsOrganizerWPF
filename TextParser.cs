using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopProjectsOrganizerWPF
{
    class TextParser
    {

        // takes .txt file
        // returns arrays
        public TextParser()
        {
        }

        public Project[] GetProjectArrayFromJson()
        {
            Project[] projects;
            try
            {
                string tmptxt = File.ReadAllText("projects.json");
                IList<ProjectJsonType> projectsJson = JsonConvert.DeserializeObject<IList<ProjectJsonType>>(tmptxt);
                projects = new Project[projectsJson.Count];

                for (int i = 0; i<projects.Length; i++)
                {
                    ProjectJsonType p = projectsJson[i];
                    string[] linkUrls = new string[p.links.Count], linkLabels = new string[p.links.Count];
                    for (int j = 0; j<p.links.Count; j++)
                    {
                        linkLabels[j] = p.links[j][0];
                        linkUrls[j] = p.links[j][1];
                    }
                    projects[i] = new Project(p.project, p.note, linkUrls, linkLabels, p.todo, p.doing, p.done); 
                }
            }
            catch
            {
                projects = new Project[1];
                string[] linkUrls = { }, linkLabels = { }, todo = { }, doing = { }, done = { };
                projects[1] = new Project("json parse fail", "", linkUrls, linkLabels, todo, doing, done);
            }

            return projects;
        }

        public Project[] GetProjectArray()
        {
            string txt = File.ReadAllText("text.txt");
            txt = txt.Replace("\r", "");
            string[] projs = txt.Split('¤');
            Project[] projects = new Project[projs.Length];

            for (int i = 0; i< projs.Length; i++)
            {
                string[] projParts = projs[i].Split('£');

                string title = projParts[0].Replace("\n","");
                string nodev = projParts[1].Replace("\n","");
                char[] seperators = new char[] { '\n' };
                string[] links = projParts[2].Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                string[] linkLabels = new string[links.Length];
                string[] linkUrls = new string[links.Length];

                for (int j = 0; j < links.Length; j++)
                {
                    string[] arr = links[j].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    linkLabels[j] = arr[0];
                    linkUrls[j] = arr[1];
                }

                string[] tasksTodo = projParts[3].Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                string[] tasksDoing = projParts[4].Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                string[] tasksDone = projParts[5].Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                projects[i] = new Project(title, nodev, linkUrls, linkLabels, tasksTodo, tasksDoing, tasksDone);
            }

            return projects;
        }
    }

    class ProjectJsonType
    {
        public string project { get; set; }
        public string note { get; set; }
        public IList<IList<string>> links { get; set; }
        public string[] todo { get; set; }
        public string[] doing { get; set; }
        public string[] done { get; set; }
    }
}
