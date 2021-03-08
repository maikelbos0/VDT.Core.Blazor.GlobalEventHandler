using Microsoft.AspNetCore.Mvc;

namespace VDT.Core.Demo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase {
        private readonly IDemo demo;

        public DemoController(IDemo demo) {
            this.demo = demo;
        }

        [HttpGet("Execute")]
        public string Execute() {
            return demo.Execute();
        }
    }
}
