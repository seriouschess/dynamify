import { ISiteComponentDto } from "./site_component_dto";

export interface ISkeletonSiteDto{
    site_id:number;
    title:string;
    site_components:ISiteComponentDto[];
}