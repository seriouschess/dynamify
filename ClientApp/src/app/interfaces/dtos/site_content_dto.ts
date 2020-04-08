import { TwoColumnBox, Image, ParagraphBox, Portrait, LinkBox } from "./site_dtos";

export interface ISiteContentDto{
    title: string;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
    link_boxes: LinkBox[];
}