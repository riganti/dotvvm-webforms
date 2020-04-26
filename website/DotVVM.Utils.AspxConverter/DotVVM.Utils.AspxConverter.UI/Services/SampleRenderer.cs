using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonMark;
using Microsoft.AspNetCore.Hosting;

namespace DotVVM.Utils.AspxConverter.UI.Services
{
    public class SampleRenderer
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private static readonly ConcurrentDictionary<string, SampleDTO> cache = new ConcurrentDictionary<string, SampleDTO>();

        public SampleRenderer(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public SampleDTO GetSampleHtml(string sampleName)
        {
            return cache.GetOrAdd(sampleName, LoadSample);
        }

        private SampleDTO LoadSample(string sampleName)
        {
            var fileName = GetSampleFileName(sampleName);
            using (var sr = new StreamReader(fileName))
            {
                var sections = ReadNextSection(sr).ToList();
                if (sections.Count != 3)
                {
                    throw new Exception($"Error in sample {sampleName}! Three sections were expected!");
                }

                return new SampleDTO()
                {
                    SectionTitle = ConvertMarkdownBlock(sections[0]),
                    WebFormsHtml = ConvertMarkdownBlock(sections[1]),
                    DotvvmHtml = ConvertMarkdownBlock(sections[2])
                };
            }
        }
        
        private string ConvertMarkdownBlock(string section)
        {
            return CommonMark.CommonMarkConverter.Convert(section);
        }

        private IEnumerable<string> ReadNextSection(StreamReader sr)
        {
            var section = new StringBuilder();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Trim().Length > 3 && line.Trim().All(t => t == '-'))
                {
                    yield return section.ToString();
                    section.Clear();
                }
                else
                {
                    section.AppendLine(line);
                }
            }

            yield return section.ToString();
        }

        private string GetSampleFileName(string sampleName)
        {
            return Path.Combine(webHostEnvironment.ContentRootPath, "Data", sampleName + ".md");
        }

    }
}