using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csharp_desktop_winform_crud
{
    public class DataService
    {
        public string path = "";

        /// <summary>
        /// Create data file
        /// </summary>
        /// 
        public void createFile()
        {
            if(!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public void deleteFile()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Insert data into file
        /// </summary>
        /// <param name="title">Post Title</param>
        /// <param name="description">Post Description</param>
        /// <returns>bool</returns>
        public bool insertData(string title,string description)
        {
            try
            {
                var posts = getPosts();
                int id =  posts.Count;
                id++;

                string data = $"{id},\"{title}\",\"{description}\"\n";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    // Append the data to the file
                    writer.WriteLine(data);
                    writer.Close();
                }
                return true;
            }catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all posts from file in list.
        /// </summary>
        /// <returns>List<Posts></returns>
        public List<Posts> getPosts()
        {
            var getPost = File.ReadAllLines(path);
            var posts = new List<Posts>();

            foreach (var item in getPost)
            {
                var spPost = item.Split(',');
                if(spPost.Count() > 1)
                {
                    Posts p = new Posts();
                    p.id = int.Parse(spPost[0]);
                    p.title = spPost[1].Replace("\"","");
                    p.description = spPost[2].Replace("\"", "");

                    posts.Add(p);
                }
            }
            return posts;
        }

        /// <summary>
        /// Re Save modified data into file
        /// </summary>
        /// <param name="posts">Post lists</param>
        private void reSaveData(List<Posts> posts)
        {
            List<string> plistStr = new List<string>();
            foreach(var p in posts)
            {
                string data = $"{p.id},\"{p.title}\",\"{p.description}\"\n";
                plistStr.Add(data);
            }
            File.WriteAllLines(path, plistStr);
        }

        /// <summary>
        /// Delete post from file by id.
        /// </summary>
        /// <param name="id">Post ID</param>
        /// <returns>bool</returns>
        public bool deletePost(int id)
        {
            var posts = getPosts();
            for (int i = 0; i < posts.Count;i++)
            {
                if (posts[i].id == id)
                {
                    posts.RemoveAt(i);
                    break;
                }
            }
            reSaveData(posts);
            return true;
        }
    }

    /// <summary>
    /// Post Class
    /// </summary>
    public class Posts
    {
        /// <summary>
        /// Post id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Post title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Post description
        /// </summary>
        public string description { get; set; }

    }
}
