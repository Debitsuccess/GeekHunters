import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { GeekService } from '../shared/geek.service';
import { Candidate } from '../shared/candidate.type';

@Component({
    selector: 'candidate',
    templateUrl: './candidate.component.html',
    styleUrls: ['./candidate.component.css']
})

export class CandidateComponent implements OnInit {
    public candidate: Candidate = new Candidate();
    public isNew: boolean = true;
    public technologyList: string[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private geekService: GeekService) {
        this.technologyList = geekService.TechnologyList;
    }

    ngOnInit(): void {
        let id = this.activatedRoute.snapshot.params['id'] as string;
        if (id !== 'new') {
            this.isNew = false;
            this.getCandidate(id);
        }
    }

    private getCandidate(id: string) {
        this.geekService.getCandidate(id)
            .subscribe(candidate => {
                this.candidate = candidate;
            });
    }

    public addCandidate() {
        this.geekService.addCandidate(this.candidate)
            .subscribe(() => {
                this.router.navigate(["candidate"]);
            });
    }

    public updateCandidate() {
        this.geekService.updateCandidate(this.candidate)
            .subscribe(() => {
                this.router.navigate(['candidate']);
            });
    }

    public navigateToList()
    {
        this.router.navigate(["candidate"]);
    }
}