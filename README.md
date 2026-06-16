Order confirmation page - important issues
=====================================

Three issues that matter most for OrderHub

1) Unsanitized HTML output (XSS risk)
- Problem: the original view used Html.Raw(Model.SchoolName), which will render raw HTML from data.
- Why it matters: an attacker or malformed input could inject scripts or markup, compromising admin browsers and user trust. OrderHub handles school and order data; protecting that surface is essential for security and compliance.

2) Full page reloads for small interactions (poor UX & performance)
- Problem: changing a line quantity submits or reloads the whole page rather than updating totals locally.
- Why it matters: admins make many quantity adjustments. Reloads slow workflows, increase server load, and risk losing unsaved context. A responsive UI that updates subtotal client-side reduces server traffic and improves productivity.

3) Fragile/unclear form structure and accessibility issues
- Problem: inputs lacked explicit types/constraints and line items used clickable divs to submit, which is semantically incorrect and harms accessibility. The original form also did not clearly include CSRF protection.
- Why it matters: incorrect HTML structure harms keyboard and screen-reader users, increases the chance of invalid input, and can introduce security/validation issues when the server expects specific data shapes. OrderHub must be reliable and accessible for school administrators.

What this repo change does
- Adds a Razor Page with a clear server-rendered structure and a small vanilla-JS layer for client-side subtotal updates when quantities change. The server still receives values on submit and re-validates them.
