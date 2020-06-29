import { TwoColumnBox, Image, ParagraphBox, Portrait, LinkBox, NavBar } from "./site_dtos";

export interface ISiteContentDto{
    title: string;
    site_id: number;
    nav_bar: NavBar;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
    link_boxes: LinkBox[];
}