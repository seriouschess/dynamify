using System;
using System.Collections.Generic;
using dynamify.Models.SiteModels;

namespace dynamify.dtos
{
    public class SiteContentDto
    {
        public string title {get;set;}
        public List<ParagraphBox> paragraph_boxes {get;set;}
        public List<Image> images {get;set;}
        public List<TwoColumnBox> two_column_boxes {get;set;}

        public List<Portrait> portraits {get;set;}
    }
}