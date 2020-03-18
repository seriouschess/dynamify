import { IGenericSiteComponent } from "./generic_site_component";

export interface ISiteFormatted{
    title: string;
    site_components: IGenericSiteComponent[];
}