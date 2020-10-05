import { LinkBox, ParagraphBox, Portrait, TwoColumnBox, Image } from "./site_dtos";

export interface ITutorialSite{
    title:string;
    site_id: number;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
    link_boxes: LinkBox[];
}