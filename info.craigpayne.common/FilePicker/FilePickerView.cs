using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.IO;
using info.craigpayne.common.IocBootstrap;

namespace info.craigpayne.common.FilePicker
{
    public class FilePickerView : IFilePickerView
    {
        private readonly Context _context;
        public delegate void DirectoryChangedEventHandler(string directory);
        public event DirectoryChangedEventHandler DirectoryChanged;

        public FilePickerView(IContext context)
        {
            _context = context.Context;
        }

        public View GetView()
        {
            return GetView(null);
        }


        public View GetView(string directory)
        {
            var outerLayout = new LinearLayout(_context) { Orientation = Orientation.Vertical };
            var layout = new LinearLayout(_context);
            var roots = directory == null ? File.ListRoots() : new[] { new File(directory) };
            layout.Orientation = Orientation.Vertical;

            var header = new TextView(_context) { Text = "Choose a file:" };
            outerLayout.AddView(header);

            var paths = new List<AndroidFilePath>();
            try
            {
                var up = roots.First().Parent;
                var text = new TextView(_context) {Text = ".. [Up]"};
                text.Click += (sender, args) => DirectoryChanged(up);
                outerLayout.AddView(text);
            }
            catch (Exception) {}
            
            foreach (var root in roots)
            {
                var fileList = root.ListFiles();
                if (fileList == null)
                    continue;
                foreach (var file in fileList)
                {
                    paths.Add(new AndroidFilePath
                    {
                        FullPath = root.Name + "/" + file.Name,
                        Path = file.Name,
                        Type = new File(root.Name + "/" + file.Name).IsDirectory ? AndroidFilePathType.Directory : AndroidFilePathType.File
                    });
                }
            }

            var sortedPaths = (from path in paths
                    orderby path.Path
                    orderby path.Type
                    select path).ToList();

            foreach (var path in sortedPaths)
            {
                var text = new TextView(_context) { Text = path.Path};
                if (path.Type == AndroidFilePathType.Directory)
                {
                    text.SetTextColor(Color.Green);
                    var fullPath = path.FullPath;
                    text.Click += (sender, args) => DirectoryChanged(fullPath);
                }
                else
                {
                    text.SetTextColor(Color.Yellow);
                }
                layout.AddView(text);
            }

            
            var scrollView = new ScrollView(_context);
            scrollView.AddView(layout);
            
            outerLayout.AddView(scrollView);
            return outerLayout;
        }
    }
}