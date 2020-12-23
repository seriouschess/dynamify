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
  selected_video:TutorialVideo;

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
    this.selected_video = null;
    this.video_list = [];
    let current_video:TutorialVideo = {
      video_title: "General guide video",
      source_url: this.dodgeTheBus("https://youtube.com/embed/aLTwNneKSSo"),
      selected: false,
      description: ""
    }

    current_video.selected = true;
    this.selected_video = current_video;
    this.video_list.push(current_video);

    //unselected videos
    current_video = {
      video_title: "Tutorial Walkthrough",
      source_url: this.dodgeTheBus("https://youtube.com/embed/yWvyYhSPLqU"),
      selected: false,
      description: ""
    }

    this.video_list.push(current_video);
    
    current_video = {
      video_title: "Account Management",
      source_url: this.dodgeTheBus("https://youtube.com/embed/7I-SSNHByvY"),
      selected: false,
      description: ""
    }

    this.video_list.push(current_video);

    current_video = {
      video_title: "How to link multipage sites",
      source_url: this.dodgeTheBus("https://www.youtube.com/embed/EAAiXQCHs8c"),
      selected: false,
      description: ""
    }

    this.video_list.push(current_video);

  }

  dodgeTheBus(str:string):SafeResourceUrl{
    return this.sanitizer.bypassSecurityTrustResourceUrl(str);
  }

  toggleVideoSelection(inc_video:TutorialVideo){
    this.video_list.forEach(
      current_video => current_video.selected = false
    );
    inc_video.selected = true;
    this.selected_video = inc_video;
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
