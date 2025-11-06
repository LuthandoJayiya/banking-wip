import { Component } from '@angular/core';

@Component({
  selector: 'app-guest-footer',
  templateUrl: './guest-footer.component.html',
  styleUrls: ['./guest-footer.component.css'],
})
export class GuestFooterComponent {
  comingSoon() {
    alert('Feature no our next sprint');
  }
}
