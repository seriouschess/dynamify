import { TwoColumnBox, Image, ParagraphBox, Portrait } from "./site_dtos";

export interface ISiteContentDto{
    title: string;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
}