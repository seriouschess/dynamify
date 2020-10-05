import { IGenericSiteComponent } from "./generic_site_component";

export interface ISiteFormatted{
    title: string;
    site_id: number;
    site_components: IGenericSiteComponent[];
}