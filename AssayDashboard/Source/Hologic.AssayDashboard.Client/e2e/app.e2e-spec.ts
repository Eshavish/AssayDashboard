import { Hologic.AssayDashboard.ClientPage } from './app.po';

describe('hologic.assay-dashboard.client App', () => {
  let page: Hologic.AssayDashboard.ClientPage;

  beforeEach(() => {
    page = new Hologic.AssayDashboard.ClientPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
