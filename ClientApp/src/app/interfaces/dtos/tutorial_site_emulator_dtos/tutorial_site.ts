import { ParagraphBox } from "../site_components/ParagraphBox";
import { Image } from "../site_components/Image";
import { TwoColumnBox } from "../site_components/TwoColumnBox";
import { Portrait } from "../site_components/Portrait";
import { LinkBox } from "../site_components/LinkBox";

export interface ITutorialSite{
    title:string;
    site_id: number;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
    link_boxes: LinkBox[];
}