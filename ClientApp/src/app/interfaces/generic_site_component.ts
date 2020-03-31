//includes any field that would exist in any of the site components
export interface IGenericSiteComponent{ 

    priority: number;
    title: string;
    site_id: number;
    content: string;
    type: string;
    image_src: string;

    heading_one:string;
    heading_two:string;
    content_one:string;
    content_two:string;
}
