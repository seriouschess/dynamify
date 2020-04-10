//site interfaces
import { Admin } from './admin_dtos';

export interface Site{
    site_id: number;
    title: string;
    active: boolean;
    admin_id: number;
    owner: Admin;
    paragraph_boxes: ParagraphBox[];
    images: Image[];
    two_column_boxes: TwoColumnBox[];
    portraits: Portrait[];
  }

export interface ParagraphBox{
    title: string;
    priority: number;
    site_id: number;
    content: string;
  }
  
  export interface Image{
    title: string;
    priority:number;
    site_id: number;
  
    image_src: string;
  }
  
  export interface Portrait{
    title: string;
    priority:number;
    site_id: number;
  
    image_src: string;
    content: string;
  }
  
  export interface TwoColumnBox{
    title:string;
    priority:number;
    site_id:number;
  
    heading_one:string;
    heading_two:string;
    content_one:string;
    content_two:string;
  }

  export interface LinkBox{
    title: string;
    priority: number;
    site_id: number;
  
    content: string;
    url: string;
    link_display: string;
  }

  export interface NavBar{
    links: NavLink[],
    site_id: number
  }

  export interface NavLink{
    url: string;
    label: string;
  }
  
