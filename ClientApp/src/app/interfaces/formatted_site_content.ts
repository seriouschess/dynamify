import { IGenericSiteComponent } from "./generic_site_component";
import{ NavBar } from "./dtos/site_dtos";

export interface ISiteFormatted{
    title: string;
    site_id: number;
    nav_bar: NavBar;
    site_components: IGenericSiteComponent[];
}