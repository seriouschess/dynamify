import { ParagraphBox } from "../site_components/paragraph_box";
import { Image } from "../site_components/image_dto";
import { TwoColumnBox } from "../site_components/two_column_box";
import { Portrait } from "../site_components/portrait_dto";
import { LinkBox } from "../site_components/link_box";

export interface ITutorialSite{
    title:string;
    site_id: number;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
    link_boxes: LinkBox[];
}