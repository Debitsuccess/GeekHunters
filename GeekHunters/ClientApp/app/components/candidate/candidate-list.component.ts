
import { Component } from '@angular/core';
import { GeekService } from '../shared/geek.service';
import { Candidate } from '../shared/candidate.type';

@Component({
    selector: 'candidate-list',
    templateUrl: './candidate-list.component.html',
    styleUrls: ['./candidate-list.component.css']
})
export class CandidateListComponent {
    public candidates: Candidate[];
    public technologyList: string[];

    constructor(private geekService: GeekService) {
        this.technologyList = geekService.TechnologyList;
        this.getCandidates();
    }

    private filterCandidates(filter: string) {
        if (filter == "All"){
            this.getCandidates();
            return;
        }

        this.geekService.getCandidates()
            .subscribe(candidates => {
                this.candidates = candidates.filter(c => this.findCandidate(c.technologies, [filter]));
            });
    }

    private findCandidate = function (haystack: string[], arr: string[]) {
        return arr.some(function (v) {
            return haystack.indexOf(v) >= 0;
        });
    };

    private getCandidates() {
        this.geekService.getCandidates()
            .subscribe(candidates => {
                this.candidates = candidates;
            });
    }

    private deleteCandidate(id: string) {
        if (confirm("Are you sure you want to delete this candidate?")) {
            this.geekService.deleteCandidate(id)
                .subscribe(() => {
                    this.getCandidates();
                });
        }
    }

    private onTechnologyChange(technology: string): void {
        this.filterCandidates(technology);
    }
}
