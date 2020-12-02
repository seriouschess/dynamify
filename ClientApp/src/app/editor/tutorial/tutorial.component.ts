import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-tutorial',
  templateUrl: './tutorial.component.html',
  styleUrls: ['./tutorial.component.css']
})
export class TutorialComponent implements OnInit {

  constructor(public sanitizer: DomSanitizer) { }

  open_site_select: boolean;
  open_editor: boolean;
  display_sites_done: boolean;
  open_guide_videos:boolean
  video_list:TutorialVideo[];

  ngOnInit() {
    this.open_site_select = false;
    this.open_editor = false;
    this.display_sites_done = false;
    this.addVideos();
  }

  beginTutorial(){
    this.open_site_select = true;
  }

  openGuideVideos(){
    this.open_guide_videos = !this.open_guide_videos;
  }

  addVideos(){
    this.video_list = [];
    let video_one:TutorialVideo = {
      video_title: "General guide video",
      source_url: this.dodgeTheBus("https://www.youtube.com/embed/TeixVm1TFQY"),
      selected: false,
      description: ""
    }

    this.video_list.push(video_one);

    let video_two:TutorialVideo = {
      video_title: "How to link multipage sites",
      source_url: this.dodgeTheBus("https://www.youtube.com/embed/EAAiXQCHs8c"),
      selected: false,
      description: ""
    }

    this.video_list.push(video_two);
  }

  dodgeTheBus(str:string):SafeResourceUrl{
    return this.sanitizer.bypassSecurityTrustResourceUrl(str);
  }

  toggleVideoSelection(inc_video:TutorialVideo){
    this.video_list.forEach(
      current_video => current_video.selected = false
    );
    inc_video.selected = true;
  }

  checkDone($event){
    this.display_sites_done = $event;
    this.open_editor = true;
    this.open_site_select = false;
  }
}

interface TutorialVideo{
  video_title: string,
  source_url: SafeResourceUrl,
  selected: boolean,
  description:string
}
